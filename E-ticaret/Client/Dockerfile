# Stage 1: Build the Angular application
FROM node:18 as build

WORKDIR /app

# Angular CLI'yi yükleyin
RUN npm install -g @angular/cli

# Dependencies yükleniyor
COPY package.json yarn.lock ./
RUN yarn install

# Uygulama kodunu kopyalıyoruz
COPY . .

# Build the application
RUN yarn build

# Stage 2: Serve the application with NGINX
FROM nginx:alpine

# Copy the build output to replace the default NGINX static files
COPY --from=build /app/dist /usr/share/nginx/html

# Expose port 80
EXPOSE 80

# Start NGINX server
CMD ["nginx", "-g", "daemon off;"]

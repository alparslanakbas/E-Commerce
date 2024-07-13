# Stage 1: Build the Angular application
FROM node:18 as build

WORKDIR /app

# Install dependencies
COPY package.json package-lock.json ./
RUN npm install

# Copy the rest of the application code
COPY . .

# Build the application
RUN npm run build

# Stage 2: Serve the application with NGINX
FROM nginx:alpine

# Copy the build output to replace the default NGINX static files
COPY --from=build /app/dist/client/browser /usr/share/nginx/html

# Expose port 80
EXPOSE 80

# Start NGINX server
CMD ["nginx", "-g", "daemon off;"]

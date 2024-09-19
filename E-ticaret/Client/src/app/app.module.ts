import { NgModule } from '@angular/core';
import { BrowserModule, Title } from '@angular/platform-browser';
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpBackend, HttpClient, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

//Routes
import { routes } from './app.route';

import { AppComponent } from './app.component';

// store
import { StoreModule } from '@ngrx/store';
import { indexReducer } from './store/index.reducer';

// shared module
import { SharedModule } from 'src/shared.module';

// i18n
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
// AOT compilation support
export function HttpLoaderFactory(httpHandler: HttpBackend): TranslateHttpLoader {
    return new TranslateHttpLoader(new HttpClient(httpHandler));
}

// dashboard
import { IndexComponent } from './index';

// Layouts
import { AppLayout } from './admin/layouts/app-layout';
import { AuthLayout } from './admin/layouts/auth-layout';

import { HeaderComponent } from './admin/layouts/header';
import { FooterComponent } from './admin/layouts/footer';
import { SidebarComponent } from './admin/layouts/sidebar';
import { ThemeCustomizerComponent } from './admin/layouts/theme-customizer';
import { CustomerComponent } from './admin/layouts/components/customer/customer.component';
import { ProductComponent } from './admin/layouts/components/product/product.component';
import { OrderComponent } from './admin/layouts/components/order/order.component';
import { DataTableModule } from '@bhplugin/ng-datatable';
import { TodoListComponent } from './admin/layouts/components/todo-list/todo-list.component';

@NgModule({
    imports: [
        RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' }),
        BrowserModule,
        BrowserAnimationsModule,
        NoopAnimationsModule,
        CommonModule,
        FormsModule,
        HttpClientModule,
        DataTableModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: HttpLoaderFactory,
                deps: [HttpBackend],
            },
        }),
        StoreModule.forRoot({ index: indexReducer }),
        SharedModule.forRoot()
    ],
    declarations: [
        AppComponent, 
        HeaderComponent, 
        FooterComponent, 
        SidebarComponent, 
        ThemeCustomizerComponent, 
        IndexComponent, 
        AppLayout, 
        AuthLayout,
        CustomerComponent,
        ProductComponent,
        OrderComponent,
        TodoListComponent
    ],
    providers: 
    [
        Title,
        {provide: "baseUrl", useValue: "https://localhost:7199/api", multi:true}
    ],
    bootstrap: [AppComponent],
})
export class AppModule {}

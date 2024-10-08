import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Title } from '@angular/platform-browser';

// service
import { AppService } from 'src/app/service/app.service';

// i18n
import { TranslateModule } from '@ngx-translate/core';

// perfect-scrollbar
import { NgScrollbarModule, provideScrollbarOptions } from 'ngx-scrollbar';

// Counter
import { CountUpModule } from 'ngx-countup';

// sortable
import { SortablejsModule } from '@dustfoundation/ngx-sortablejs';

// modal
import { NgxCustomModalComponent } from 'ngx-custom-modal';

// highlightjs
import { provideHighlightOptions } from 'ngx-highlightjs';

// headlessui
import { MenuModule } from 'headlessui-angular';

// icons
import { IconModule } from 'src/app/shared/icon/icon.module';
import { NgxFileDropModule } from 'ngx-file-drop';

@NgModule({
    imports: [
        CommonModule,
        FormsModule, 
        ReactiveFormsModule, 
        RouterModule, 
        TranslateModule.forChild(), 
        NgScrollbarModule, 
        MenuModule, 
        IconModule,
        CountUpModule, 
        SortablejsModule,
        NgxCustomModalComponent,
        NgxFileDropModule
    ],
    declarations: [],
    exports: [
        // modules
        FormsModule,
        ReactiveFormsModule,
        TranslateModule,
        NgScrollbarModule,
        MenuModule,
        IconModule,
        CountUpModule,
        SortablejsModule,
        NgxCustomModalComponent,
        NgxFileDropModule
    ],
})

export class SharedModule {
    static forRoot(): ModuleWithProviders<any> {
        return {
            ngModule: SharedModule,
            providers: [
                Title,
                AppService,
                provideScrollbarOptions({
                    visibility: 'hover',
                    appearance: 'compact',
                }),
                provideHighlightOptions({
                    coreLibraryLoader: () => import('highlight.js/lib/core'),
                    languages: {
                        json: () => import('highlight.js/lib/languages/json'),
                        typescript: () => import('highlight.js/lib/languages/typescript'),
                        xml: () => import('highlight.js/lib/languages/xml'),
                    },
                }),
            ],
        };
    }
}
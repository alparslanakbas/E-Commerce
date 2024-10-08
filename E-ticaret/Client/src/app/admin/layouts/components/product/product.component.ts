import { Component, OnInit, Output, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Product } from 'src/app/models/product';
import { HttpClientService } from 'src/app/service/http-client.service';
import Swal from 'sweetalert2';
import { ProductService } from './product.service';
import { FileUploadOptions } from 'src/app/service/file-upload/file-upload.component';

@Component({
    selector: 'app-product',
    templateUrl: './product.component.html',
    styleUrls: ['./product.component.css'],
})
export class ProductComponent implements OnInit {
    currentPath: string | undefined;
    products: Product[] = [];
    product: Product = new Product();
    indexCount: number = 0;
    nowDate = new Date();

    totalProductCount: number = 0;
    displayedProducts: any[] = [];
    currentPage: number = 1;
    pageSize: number = 20;
    paginationInfo: string = '';
    totalPages: number = 0;

    filterName: string = '';
    filterId: string = '';
    filterStock: string = '';
    filterPrice: string = '';
    filterCreatedDate: string = '';
    filterUpdatedDate: string = '';

    loading: boolean = true;
    loadingTableButtons = true;

    errorMessages: string = '';


    @Output() options: Partial<FileUploadOptions> ={
        action: "upload",
        controller: "product",
        explanation: "Resimleri seçin",
        accept: ".jpg, .png" 
    }

    constructor(
        private route: ActivatedRoute,
        private httpClient: HttpClientService,
        private translate: TranslateService,
        private productService: ProductService,
    ) {}

    ngOnInit() {
        this.route.url.subscribe((url) => {
            if (url.length > 1) {
                this.currentPath = url[2].path;
            } else {
                this.currentPath = 'Dashboard';
            }
        });

        this.loadTranslations();
        this.list(this.currentPage, this.pageSize);
    }

    @ViewChild('datatable') datatable: any;
    search = '';
    cols = [
        { field: 'index', title: '#' },
        { field: 'id', title: 'Id' },
        { field: 'name', title: 'Name' },
        { field: 'stock', title: 'Stock' },
        { field: 'price', title: 'Price' },
        { field: 'createdDate', title: 'Created Date' },
        { field: 'updatedDate', title: 'Updated Date' },
        { field: 'actions', title: 'Actions', sort: false, headerClass: 'justify-center' },
    ];

    loadTranslations() {
        setTimeout(() => {
            this.cols.forEach((col, index) => {
                switch (col.field) {
                    case 'index':
                        this.cols[index].title = this.translate.instant('index');
                        break;
                    case 'id':
                        this.cols[index].title = this.translate.instant('id');
                        break;
                    case 'name':
                        this.cols[index].title = this.translate.instant('name');
                        break;
                    case 'stock':
                        this.cols[index].title = this.translate.instant('stock');
                        break;
                    case 'price':
                        this.cols[index].title = this.translate.instant('price');
                        break;
                    case 'createdDate':
                        this.cols[index].title = this.translate.instant('createdDate');
                        break;
                    case 'updatedDate':
                        this.cols[index].title = this.translate.instant('updatedDate');
                        break;
                    case 'actions':
                        this.cols[index].title = this.translate.instant('actions');
                        break;
                }
            });
        }, 0);
    }

    exportTable(type: string) {
        let columns: any = this.cols
            .filter((col) => col.field !== 'id' && col.field !== 'actions')
            .map((d: { field: any; title: string }) => {
                return { field: d.field, title: d.title };
            });

        const translateProduct = this.translate.instant('products');
        const translateTables = this.translate.instant('little_tables');
        const formattedDate =
            this.nowDate.getFullYear() +
            '-' +
            ('0' + (this.nowDate.getMonth() + 1)).slice(-2) +
            '-' +
            ('0' + this.nowDate.getDate()).slice(-2) +
            '-' +
            ('0' + this.nowDate.getHours()).slice(-2) +
            ':' +
            ('0' + this.nowDate.getMinutes()).slice(-2);
        let records = this.products;
        let filename = translateProduct + '-' + formattedDate + '-' + translateTables;

        let newVariable: any;
        newVariable = window.navigator;

        // CSV Çıktısı
        if (type == 'csv') {
            let coldelimiter = ';';
            let linedelimiter = '\n';
            let result = columns
                .map((d: any) => {
                    return d.title;
                })
                .join(coldelimiter);
            result += linedelimiter;

            // Güncel tablo verilerini kullan
            let recordsToExport = this.displayedProducts.length > 0 ? this.displayedProducts : this.products; // Mevcut gösterilen veya tamamı

            recordsToExport.map((item: { [x: string]: any }) => {
                columns.map((d: any, index: number) => {
                    if (index > 0) {
                        result += coldelimiter;
                    }
                    let val = item[d.field] ? item[d.field] : '';
                    result += val;
                });
                result += linedelimiter;
            });

            if (result == null) return;
            if (!result.match(/^data:text\/csv/i) && !newVariable.msSaveOrOpenBlob) {
                var data = 'data:application/csv;charset=utf-8,' + encodeURIComponent(result);
                var link = document.createElement('a');
                link.setAttribute('href', data);
                link.setAttribute('download', filename + '.csv');
                link.click();
            } else {
                var blob = new Blob([result]);
                if (newVariable.msSaveOrOpenBlob) {
                    newVariable.msSaveBlob(blob, filename + '.csv');
                }
            }
        }

        // Print Çıktısı
        else if (type == 'print') {
            var rowhtml = '<p>' + filename + '</p>';
            rowhtml +=
                '<table style="width: 100%; " cellpadding="0" cellspacing="0"><thead><tr style="color: #515365; background: #eff5ff; -webkit-print-color-adjust: exact; print-color-adjust: exact; "> ';
            columns.map((d: any) => {
                rowhtml += '<th>' + d.title + '</th>';
            });
            rowhtml += '</tr></thead>';
            rowhtml += '<tbody>';

            // Güncel tablo verilerini kullan
            let recordsToExport = this.displayedProducts.length > 0 ? this.displayedProducts : this.products; // Mevcut gösterilen veya tamamı

            recordsToExport.map((item: { [x: string]: any }) => {
                rowhtml += '<tr>';
                columns.map((d: any) => {
                    let val = item[d.field] ? item[d.field] : '';
                    rowhtml += '<td>' + val + '</td>';
                });
                rowhtml += '</tr>';
            });

            rowhtml +=
                '<style>body {font-family:Arial; color:#495057;}p{text-align:center;font-size:18px;font-weight:bold;margin:15px;}table{ border-collapse: collapse; border-spacing: 0; }th,td{font-size:12px;text-align:left;padding: 4px;}th{padding:8px 4px;}tr:nth-child(2n-1){background:#f7f7f7; }</style>';
            rowhtml += '</tbody></table>';
            var winPrint: any = window.open('', '', 'left=0,top=0,width=1000,height=600,toolbar=0,scrollbars=0,status=0');
            winPrint.document.write('<title>' + filename + '</title>' + rowhtml);
            winPrint.document.close();
            winPrint.focus();
            winPrint.onafterprint = () => {
                winPrint.close();
            };
            winPrint.print();
        }

        // TXT Çıktısı
        else if (type == 'txt') {
            let coldelimiter = ',';
            let linedelimiter = '\n';
            let result = columns
                .map((d: any) => {
                    return d.title;
                })
                .join(coldelimiter);
            result += linedelimiter;

            // Güncel tablo verilerini kullan
            let recordsToExport = this.displayedProducts.length > 0 ? this.displayedProducts : this.products; // Mevcut gösterilen veya tamamı

            recordsToExport.map((item: { [x: string]: any }) => {
                columns.map((d: any, index: number) => {
                    if (index > 0) {
                        result += coldelimiter;
                    }
                    let val = item[d.field] ? item[d.field] : '';
                    result += val;
                });
                result += linedelimiter;
            });

            if (result == null) return;
            if (!result.match(/^data:text\/txt/i) && !newVariable.msSaveOrOpenBlob) {
                var data = 'data:application/txt;charset=utf-8,' + encodeURIComponent(result);
                var link = document.createElement('a');
                link.setAttribute('href', data);
                link.setAttribute('download', filename + '.txt');
                link.click();
            } else {
                var blob = new Blob([result]);
                if (newVariable.msSaveOrOpenBlob) {
                    newVariable.msSaveBlob(blob, filename + '.txt');
                }
            }
        }
    }

    capitalize(text: string) {
        return text
            .replace('_', ' ')
            .replace('-', ' ')
            .toLowerCase()
            .split(' ')
            .map((s: string) => s.charAt(0).toUpperCase() + s.substring(1))
            .join(' ');
    }

    deleteRow(item: any = null) {
        // let selectedRows = this.datatable.getSelectedRows();
        // if (!selectedRows.length && !item) {

        //     this.translate.get('ALERT.NO_SELECTION').subscribe((noSelectionText: string) => {
        //         Swal.fire('Warning', noSelectionText, 'warning');
        //     });
        //     return;  Buraya api den veriler getirilirken bakılacak.!!
        // }

        this.translate
            .get([
                'ALERT.TITLE',
                'ALERT.TEXT',
                'ALERT.CONFIRM',
                'ALERT.CANCEL',
                'ALERT.SUCCESS_TITLE',
                'ALERT.SUCCESS_TEXT',
                'ALERT.CANCELLED_TITLE',
                'ALERT.CANCELLED_TEXT',
            ])
            .subscribe((translations) => {
                Swal.fire({
                    title: translations['ALERT.TITLE'],
                    text: translations['ALERT.TEXT'],
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonText: translations['ALERT.CONFIRM'],
                    cancelButtonText: translations['ALERT.CANCEL'],
                }).then((result) => {
                    if (result.isConfirmed) {
                        if (item) {
                            this.products = this.products.filter((d: any) => d.id != item);
                            this.datatable.clearSelectedRows();
                            Swal.fire(translations['ALERT.SUCCESS_TITLE'], translations['ALERT.SUCCESS_TEXT'], 'success');
                        } else {
                            let selectedRows = this.datatable.getSelectedRows();
                            const ids = selectedRows.map((d: any) => {
                                return d.id;
                            });
                            this.products = this.products.filter((d: any) => !ids.includes(d.id as never));
                            this.datatable.clearSelectedRows();
                            Swal.fire(translations['ALERT.SUCCESS_TITLE'], translations['ALERT.SUCCESS_TEXT'], 'success');
                        }
                    } else if (result.dismiss === Swal.DismissReason.cancel) {
                        Swal.fire(translations['ALERT.CANCELLED_TITLE'], translations['ALERT.CANCELLED_TEXT'], 'error');
                    }
                });
            });
    }

    // Sunucu taraflı sayfa değişimi
    onChangeServer(event: any) {
        const { current_page, pagesize } = event;

        if (current_page && pagesize) {
            this.currentPage = current_page;
            this.pageSize = pagesize;
            this.list(this.currentPage, this.pageSize);
        } else {
            console.log('Sayfa veya sayfa boyutu undefined döndü.');
        }
    }

    // Pagination bilgilerini güncelle
    setPagination() {
        const displayStart = (this.currentPage - 1) * this.pageSize + 1;
        const displayEnd = Math.min(this.currentPage * this.pageSize, this.totalProductCount);
        this.paginationInfo = `Showing ${displayStart} to ${displayEnd} of ${this.totalProductCount} entries`;
    }

    // Sayfa değişimlerini işleyin
    onPageChange(newPage: number) {
        if (newPage > 0 && newPage <= this.totalPages) {
            this.currentPage = newPage;
            
        }
    }

    // Sayfa boyutunu değiştir
    onPageSizeChange(newSize: number) {
        this.pageSize = newSize;
    }

    // Ürünleri listeleme fonksiyonu
    list(pageNumber: number = this.currentPage, pageSize: number = this.pageSize) {
        let queryString = `page=${pageNumber}&size=${pageSize}`;
        this.httpClient
            .get<{ data: Product[], totalItems: number }>({
                controller: 'Product',
                action: 'GetAll',
                queryString: queryString,
            })
            .subscribe({
                next: (response) => {
                    this.products = response.data.map((product, index) => ({
                        ...product,
                        index: (pageNumber - 1) * pageSize + index + 1,  // Index sıralaması
                    }));
                    this.totalProductCount = response.totalItems;
                    this.totalPages = Math.ceil(this.totalProductCount / pageSize);
                    this.setPagination();
                    this.loading = false;
                },
                error: (err) => {
                    console.log('Hata:', err);
                    this.loading = false;
                },
            });
    }
    

    // Add
    add() {
        this.productService.add(
            this.product,
            () => {
                Swal.fire({
                    icon: 'success',
                    title: this.translate.instant('Success'),
                    text: this.translate.instant('Product added successfully'),
                });
                this.list(this.currentPage, this.pageSize);
                this.product = new Product();
            },
            (errorMessage: string) => {
                this.errorMessages = errorMessage;
                Swal.fire({
                    icon: 'error',
                    title: this.translate.instant('Error'),
                    html: this.errorMessages,
                });
            },
        );
    }
}

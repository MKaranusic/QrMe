export class Constants {
    static readonly SessionKey = {
        HideDownloadBanner: 'HideDownloadBanner'
    };

    static readonly Role = {
        Admin: 'ADMIN',
        Customer: 'CUSTOMER'
    };

    static readonly Routes = {
        Login: {
            Path: '',
            RouterLink: ''
        },
        NotSetLinkComponent: {
            Path: 'not-set-url',
            RouterLink: 'not-set-url'
        },
        Admin: {
            Path: 'admin',
            Children: {
                CreateCustomer: {
                    Name: 'Create',
                    Path: 'create-customer',
                    RouterNavigateLink: 'admin/create-customer'
                }
            }
        },
        Customer: {
            Path: 'customer',
            Children: {
                Home: {
                    Name: 'Home',
                    Path: '',
                    RouterNavigateLink: 'customer/'
                },
                NewQrRedirect: {
                    Name: 'New Qr Redirect',
                    Path: 'new-qr-redirect',
                    RouterNavigateLink: 'customer/new-qr-redirect'
                },
                EditQrRedirect: {
                    Name: 'Edit Qr Redirect',
                    Path: 'edit-qr-redirect',
                    RouterNavigateLink: 'customer/edit-qr-redirect'
                }
            }
        }
    };
}

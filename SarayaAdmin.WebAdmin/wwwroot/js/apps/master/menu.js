const paginate = Vue.component('paginate', VuejsPaginate);

const masterMenu = new Vue({
    el: '#masterMenu',
    components: {
        paginate
    },
    data: {
        formType: 0,
        form: {
            title: null,
            url: null,
            status: false,
            parent: null,
            id: 0,
            cbStatus: "0"
        },
        menuList: {
        },
        validate: {
            title: false,
            url: false
        },
        cbMenuList: {},
        showForm: false,
        btnShowModal: null,
        formTitle: null,
        searchTerm: null,
        paging: {
            page: 0,
            number: 0
        },
        selectAll: false,
        checkedList: []
    },
    mounted() {
        axios.all([this.getAll(), this.getCbMenu()]);
        document.title = `${this.$lang.lblmenu_menu}`;
    },
    created() {

    },
    computed: {
        titleValidation: function () {
            if (this.form.title === null || this.form.title === '')
                return true;

            return false;
        },
        urlValidation: function () {
            if (this.form.url === null || this.form.url === '')
                return true;

            return false;
        }
    },
    methods: {
        createClick: function () {
            this.formType = form_add;
            this.showForm = true;
            this.formTitle = 'Buat Menu';
            this.formReset();
        },
        editClick: function (item) {
            this.formType = form_edit;
            this.showForm = true;
            this.formTitle = 'Ubah Menu';
            this.setForm(item);
            this.form.cbStatus = item.status === false ? '0' : '1'
        },
        viewClick: function (item) {
            this.formType = form_view;
            this.showForm = true;
            this.formTitle = 'Lihat Menu';
            this.setForm(item);
            this.form.cbStatus = item.status === false ? '0' : '1'
        },
        deleteClick: function ({ id }) {
            let self = this;
            let url = '/menu';
            let param = {
                id: id
            };
            const callback = function () {
                self.getAll();
            }

            self.$confirmDelete(url, param, callback);
        },
        deleteAllAction: function () {
            let self = this;
            let url = '/menu/delete-list';
            let param = {
                id: self.checkedList
            }
            const callback = function () {
                self.getAll();
            }

            self.$confirmDelete(url, param, callback, true);
        },
        onSubmit: function () {
            this.form.status = this.form.cbStatus === '0' ? false : true;
            this.form.parent = this.form.parent === "" ? null : this.form.parent;

            if (this.formType === 0) {
                axios.post('/menu', this.form)
                    .then((response) => {
                        let status = response.data.status ? 1 : 0;
                        this.$alertMessage(response.data.message, status, this.formCloseCallback(status));
                        if (response.data.status)
                            this.getAll();
                    }).catch((response) => {
                        this.$alertMessage(ERROR_MESSAGE, 0, this.formCloseCallback(0));
                    });
            } else {
                axios.put(`/menu/${this.form.id}`, this.form)
                    .then(({ data }) => {
                        this.$alertMessage(data.message, 1, this.formCloseCallback(1));
                        this.getAll();
                    }).catch(() => {
                        this.$alertMessage(ERROR_MESSAGE, 0, this.formCloseCallback(0));
                    });
            }

        },
        getAll: function (page) {
            let param = {
                index: page
            };
            if (page === null || page === undefined)
                param = {};

            axios.get('/menu', {
                params: param
            }).then(({ data }) => {
                this.menuList = data.listData.items;
                this.paging.page = data.listData.pages;
            });
        },
        getCbMenu: function () {
            axios.get('/menu', {
                params: {
                    limit: 50
                }
            }).then(({ data }) => {
                this.cbMenuList = data.listData.items;
            });
        },
        formCloseCallback: function (type) {
            if (type !== 0) {
                this.formCancel();
                this.formReset();
            }
        },
        formCancel: function () {
            this.showForm = false;
        },
        formReset: function () {
            this.form.title = '';
            this.form.url = '';
            this.form.parent = '';
            this.form.cbStatus = '0';
        },
        searchMenu: function () {
            axios.get('/menu', {
                params: {
                    title: this.searchTerm
                }
            }).then(({ data }) => {
                this.menuList = data.listData.items;
            });
        },
        setForm: function ({ id, title, url, parent, status }) {
            this.form.id = id;
            this.form.title = title;
            this.form.url = url;
            this.form.parent = parent;
            this.form.cbStatus = status ? '1' : '0'
        },
        pagingClick: function (pageNumber) {
            this.getAll(pageNumber);
        },
        checkboxAllChange: function () {
            let self = this;
            self.menuList.forEach(function (value, index) {
                if (self.selectAll)
                    self.checkedList.push(value.id);
            });
            if (!self.selectAll)
                self.checkedList = [];
        },
        checkboxChange: function () {
            let self = this;
            if (self.menuList.length === self.checkedList.length)
                self.selectAll = true;
            else
                self.selectAll = false;
        },
        selectAction: function (event) {
            let self = this;

            if (parseInt(event.target.value) === form_delete)
                self.deleteAllAction();
        }
    }
});
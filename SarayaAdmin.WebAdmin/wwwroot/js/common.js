"use strict";

const form_add = 0;
const form_edit = 1;
const form_view = 2;
const form_delete = 3;
const password_length = 8;
const lang = Vue.prototype.$lang;
const baseUrl = 'https://localhost:5001';

axios.defaults.baseURL = '/api/';
if ($cookies.get('token') !== null)
    axios.defaults.headers.common['Authorization'] = `Bearer ${$cookies.get('token')}`;
axios.defaults.withCredentials = true;

Vue.use(window.vuelidate.default);

Vue.prototype.$alertMessage = function (message, type, callback) {
    let title = null;
    if (callback === null || callback === undefined)
        callback = () => { };

    switch (type) {
        case 0:
            title = lang.label_error;
            break;
        case 1:
            title = lang.label_success;
            break;
        case 2:
            title = lang.label_info;
            break;
    }

    swal({
        title: title,
        text: message,
        icon: title.toLowerCase(),
        buttons: false,
        dangerMode: false,
    }).then(callback);
}

Vue.prototype.$confirmDelete = function (url, urlParams, responseCallback, isParamList = false) {
    let params = {};

    if (urlParams == null) {
        urlParams = {};
    }

    if (isParamList) {
        params.params = decodeURIComponent($.param(urlParams, true));
    } else {
        params.params = urlParams;
    }

    swal(lang.dialog_delete, {
        buttons: {
            cancel: lang.action_cancel,
            catch: {
                text: lang.action_delete,
                value: "submit",
            }
        },
    }).then((value) => {
        switch (value) {
            case "submit":
                axios.delete(url, params).then(({ data }) => {
                    let status = data.status ? 1 : 0;
                    Vue.prototype.$alertMessage(data.message, status, responseCallback);
                }).catch((response) => {
                    Vue.prototype.$alertMessage(lang.ERROR0000, 0, responseCallback);
                });
                break;
        }
    });
}


const header = new Vue({
    el: '#header',
    data: {
        username: $cookies.get("username"),
        locale: $cookies.get("lang")
    },
    methods: {
        logout: function () {
            axios.post('/auth/logout')
                .then((response) => {
                    if (response.status === 200) {
                        this.$cookies.remove('token');
                        this.$cookies.remove('username');

                        let url = window.location.pathname;
                        let splitUrl = url.split('/');
                        if (splitUrl[1] === 'system') {
                            window.location.replace(`${baseUrl}/login`);
                        } else {
                            window.location.replace(`${baseUrl}`);
                        }
                    }
                });
        },
        changeLang: function (lang) {
            $cookies.set('lang', lang);
            window.location.reload();
        }
    }
});

const menuNav = new Vue({
    el: '#menu-navbar'
})
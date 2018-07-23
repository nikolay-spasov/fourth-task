(function () {
    'use strict';

    new Vue({
        el: '#app',
        http: {
            root: 'http://localhost:57145/api/customers'
        },
        data: {
            customers: [],
            term: null
        },
        methods: {
            customerSelected: function (customerId) {
                window.location.href = '/Home/CustomerDetails/' + customerId;
            },
            search: function () {
                var url = '';
                if (this.term) {
                    url += '?term=' + this.term;
                }

                var data = this;
                this.$http.get(url).then(function (response) {
                    data.customers = response.body;
                });
            }
        },
        beforeMount: function () {
            this.search();
        }
    });
})();
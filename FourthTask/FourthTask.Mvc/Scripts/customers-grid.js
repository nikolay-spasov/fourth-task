(function () {
    'use strict';

    new Vue({
        el: '#app',
        data: {
            customers: [],
            term: null
        },
        methods: {
            customerSelected: function (customerId) {
                window.location.href = '/Home/Customer/' + customerId;
            },
            search: function () {
                var url = 'http://localhost:57145/api/customers';
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
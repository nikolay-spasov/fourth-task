'use strict';
    var baseUrl = 'http://localhost:57145/api/customer/';

    new Vue({
        el: '#app',
        data: {
            customer: {},
            orders: [],
            customerId: null,
        },
        methods: {
            getCustomer: function () {
                var vm = this;
                this.$http.get(baseUrl + this.customerId)
                    .then(function (response) {
                        if (response.status === 200) {
                            vm.customer = response.body;
                            vm.getOrders();
                        } else {
                            console.log('No such customer');
                        }
                    });
            },
            getOrders: function () {
                var vm = this;
                this.$http.get(baseUrl + this.customerId + '/orders')
                    .then(function (response) {
                        if (response.status === 200) {
                            vm.orders = response.body;
                        }
                    });
            },
        },
        filters: {
            formatDate: function (value) {
                if (!value) return value;
                return new Date(value).toLocaleDateString("en-US");
            },
            emptyString: function (value) {
                if (value) return value;

                return '-';
            }
        },
        beforeMount: function () {
            this.customerId = this.$el.querySelector('#customerId').value;
            this.getCustomer();
        }
    });


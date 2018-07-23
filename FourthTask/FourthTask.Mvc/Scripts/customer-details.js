(function () {
    'use strict';
    new Vue({
        el: '#app',
        http: {
            root: 'http://localhost:57145/api/customer'
        },
        data: {
            customer: {},
            orders: [],
            customerId: null
        },
        methods: {
            getCustomer: function () {
                var vm = this;
                this.$http.get(this.customerId)
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
                this.$http.get(this.customerId + '/orders')
                    .then(function (response) {
                        if (response.status === 200) {
                            vm.orders = response.body;
                        }
                    });
            }
        },
        filters: {
            formatDate: function (value) {
                if (!value) return value;
                return new Date(value).toLocaleDateString("en-US");
            },
            emptyString: function (value) {
                if (value) return value;

                return '-';
            },
            price: function (value) {
                if (typeof value === 'number') {
                    return '$' + value.toFixed(2);
                }

                return value;
            }
        },
        beforeMount: function () {
            this.customerId = this.$el.querySelector('#customerId').value;
            this.getCustomer();
        }
    });
})();

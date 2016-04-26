angular.module('app')
    .factory('LeaseResource', [
        '$resouce',
        'apiUrl',
        function ($resource, apiUrl) {

            var resource = $resource(
                apiUrl + '/leases', {}, {
                    createLease: {
                        method: 'POST'
                    },

                    getLeases: {
                        method: 'GET',
                        isArray: true
                    },

                    update: {
                        method: 'PUT'
                    }
                });

            return {
                createLease: function(leaseId) {
                    return resource.createLease({ id: leaseId });
                },

                getLeases: function() {
                    return resource.getLeases();
                },

                update: function(updatedLeaseData) {
                    return resource.update(updatedLeaseData);
                }
            };
        }]
);
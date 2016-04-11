angular.module('app')
    .factory('PropertyResource', [
        '$resource',
        'apiUrl',
        function ($resource, apiUrl) {

            var resource = $resource(
                apiUrl + '/properties', {}, {
                    createProperty: {
                        method: 'POST'
                    },

                    update: {
                        method: 'PUT'
                    }
                });
        }]
);
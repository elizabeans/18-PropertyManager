angular.module('app')
    .factory('PropertyResource', [
        '$resource',
        'apiUrl',
        function($resource, apiUrl) {

            var resource = $resource(
                apiUrl + '/properties', {}, {
                    createProperty: {
                        method: 'POST'
                    },

                    getProperties: {
                        method: 'GET',
                        isArray: true
                    },

                    update: {
                        method: 'PUT'
                    }
                });

            return {
                createProperty: function(propertyId) {
                    return resource.createProperty({ id: propertyId });
                },

                getProperties: function() {
                    return resource.getProperties();
                },

                update: function(updatedPropertyData) {
                    return resource.update(updatedPropertyData);
                }
            };
        }]
);
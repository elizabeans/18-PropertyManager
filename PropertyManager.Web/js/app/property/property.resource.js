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

                    update: {
                        method: 'PUT'
                    }
                });

            return {
                createProperty: function(propertyId) {
                    return resource.createProperty({ id: propertyId });
                },

                update: function(updatedPropertyData) {
                    return resource.update(updatedPropertyData);
                }
            };
        }]
);
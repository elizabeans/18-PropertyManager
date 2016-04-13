angular.module('app').controller('PropertyAddController', function ($scope, AddressResource, PropertyResource, apiUrl) {

    $scope.newProperty = {};

    $scope.createProperty = function(newPropertyData) {
        PropertyResource.createProperty(newPropertyData).$promise
            .then(function(data) {
                alert("Property created!");
            }).catch(function(err) {
                alert("Error creating property");
            });
    };

});
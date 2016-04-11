angular.module('app').controller('PropertyAddController', function ($scope, AddressResource, PropertyResource, apiUrl) {

    $scope.data = {
        newAddress: {},
        newProperty: {}
    }

    $scope.saveProperty = function () {
        PropertyResource.create($scope.newProperty, function () {
            addAddress(newProperty, function(data) {
                alert('New Property Added!');
            });
        });
    };

    $scope.addAddress = function (property) {
        property.newAddress.PropertyId = property.PropertyId;
        AddressResource.create(property.newAddress);
    };

});
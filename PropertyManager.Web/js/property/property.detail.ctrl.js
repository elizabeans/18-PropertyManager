angular.module('app').controller('PropertyDetailController', function ($scope, $stateParams, PropertyResource) {
    console.log($stateParams);

    $scope.property = PropertyResource.get({ propertyId: $stateParams.id });

    $scope.saveProperty = function () {
        $scope.property.$update(function () {
            alert('Save successful.');
        });
    };
});
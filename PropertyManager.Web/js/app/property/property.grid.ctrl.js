angular.module('app').controller('PropertyGridController', function($scope, PropertyResource) {

    $scope.properties = [];

    function activate() {
        PropertyResource.getProperties().$promise
            .then(function(properties) {
                $scope.properties = properties;
            });
    };

    activate();

    $scope.removeProperty = function (property) {
        property.$remove(function (data) {
            activate();
        });
    };

});
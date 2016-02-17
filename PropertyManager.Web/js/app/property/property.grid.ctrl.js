angular.module('app').controller('PropertyGridController', function($scope, PropertyResource){

    function activate() {
        $scope.properties = PropertyResource.query();
    }

    activate();

    $scope.removeProperty = function (property) {
        property.$remove(function (data) {
            activate();
        });
    };

});
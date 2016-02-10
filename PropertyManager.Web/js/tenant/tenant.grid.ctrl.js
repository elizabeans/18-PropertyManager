angular.module('app').controller('TenantGridController', function ($scope, TenantResource) {

    function activate() {
        $scope.tenants = TenantResource.query();
    }

    activate();

    $scope.removeTenant = function (tenant) {
        tenant.$remove(function (data) {
            activate();
        });
    };
});
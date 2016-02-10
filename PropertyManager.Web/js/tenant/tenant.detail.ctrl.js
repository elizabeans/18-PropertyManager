angular.module('app').controller('TenantDetailController', function ($scope, $stateParams, TenantResource) {
    console.log($stateParams);

    $scope.tenant = TenantResource.get({ tenantId: $stateParams.id });

    $scope.saveTenant = function () {
        $scope.tenant.$update(function () {
            alert('Save successful.');
        });
    };
});
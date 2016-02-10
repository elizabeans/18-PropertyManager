angular.module('app').controller('LeaseDetailController', function ($scope, $stateParams, LeaseResource) {

    console.log($stateParams);

    $scope.lease = LeaseResource.get({ leaseId: $stateParams.id });

    $scope.saveLease = function () {
        $scope.lease.$update(function () {
            alert('Save successful.');
        });
    };
});
angular.module('app').controller('WorkorderDetailController', function ($scope, $stateParams, WorkorderResource) {
    console.log($stateParams);

    $scope.workorder = WorkorderResource.get({ workorderId: $stateParams.id });

    $scope.saveWorkorder = function () {
        $scope.workorder.$update(function () {
            alert('Save successful.');
        });
    };
});
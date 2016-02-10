angular.module('app').controller('WorkorderGridController', function ($scope, WorkorderResource) {

    function activate() {
        $scope.workorders = WorkorderResource.query();
    }

    activate();

    $scope.removeWorkorder = function (workorder) {
        workorder.$remove(function (data) {
            activate();
        });
    };
});
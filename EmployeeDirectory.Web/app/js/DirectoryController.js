(function (app) {
    'use strict';

    app.controller('DirectoryController', ['$scope', '$modal', '$state', 'dataService', 'searchService', DirectoryController]);

    function DirectoryController($scope, $modal, $state, dataService, searchService) {
        $scope.employees = [];
        $scope.totalEmployees = 0;
        $scope.currentPage = 1;
        $scope.pageSize = 25;

        $scope.pageChanged = function () {
            searchService.applyCurrentQueryToPages($scope.currentPage, $scope.pageSize);
        };

        $scope.$on('searchResultsChanged', function (event, queryResult) {
            $scope.employees = queryResult.employees;
            $scope.totalEmployees = queryResult.totalCount;
        });

        $scope.editEmployee = function (employeeId) {
            $state.go('employee-detail', { id: employeeId });
        };

        $scope.deleteEmployee = function (index) {
            //TODO - implement a confirmation box, could use window.confirm but rather you a modal
            //set is deleting flag to disable all other delete buttons until this one returns?  two delete operations returning out of order will mess up splicing the $scope.employees array
            //TODO - rather probably want to remove immediately, and add it back in only on failure - more responsive.

            dataService.deleteEmployee($scope.employees[index].employeeId)
                .then(function () {
                    $scope.employees.splice(index, 1);
                });
        };

        //unless coming from employee-detail or back? OR maybe use querystring?
        searchService.query(1, $scope.pageSize);
    }
})(angular.module('app'));
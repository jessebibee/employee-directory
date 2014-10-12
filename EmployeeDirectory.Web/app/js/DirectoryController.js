(function (app) {
    'use strict';

    app.controller('DirectoryController', ['$scope', '$modal', '$state', 'dataService', 'searchService', DirectoryController]);

    function DirectoryController($scope, $modal, $state, dataService, searchService) {
        $scope.employees = [];
        $scope.totalEmployees = 0; //reference searchService directly?
        $scope.currentPage = 1; //reference searchService directly?
        $scope.pageSize = 25; //reference searchService directly? //TODO - a directive can manage this above the grid

        $scope.pageChanged = function () {
            console.log('Page changed to: ' + $scope.currentPage);
        };

        $scope.$on('searchResultsChanged', function (event, queryResult) {
            $scope.employees = queryResult.employees;
            $scope.totalEmployees = queryResult.count;
            $scope.currentPage = 1;
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

        //initial load
        if (searchService.lastQueryResult === null) {
            searchService.search(1, $scope.pageSize);
        }
    }
})(angular.module('app'));
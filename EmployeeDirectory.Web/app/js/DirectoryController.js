(function (app) {
    'use strict';

    app.controller('DirectoryController', ['$scope', '$window', '$state', 'dataService', 'searchService', DirectoryController]);

    function DirectoryController($scope, $window, $state, dataService, searchService) {
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
            var confirmMessage = 'Are you you sure you want to delete ' + $scope.employees[index].firstName + ' ' + $scope.employees[index].lastName + '?';
            if ($window.confirm(confirmMessage)) {
                dataService.deleteEmployee($scope.employees[index].employeeId)
                    .then(function () {
                        $scope.employees.splice(index, 1);
                    });
            }
        };

        //unless coming from employee-detail or back? OR maybe use querystring?
        searchService.query(1, $scope.pageSize);
    }
})(angular.module('app'));
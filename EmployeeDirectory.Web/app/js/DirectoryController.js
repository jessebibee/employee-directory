(function (app) {
    'use strict';

    app.controller('DirectoryController', ['$scope', '$window', '$state', 'dataService', 'searchService', DirectoryController]);

    function DirectoryController($scope, $window, $state, dataService, searchService) {
        $scope.employees = [];
        $scope.totalEmployees = 0;
        $scope.currentPage = 1;
        $scope.pageSize = 25;
        $scope.isSearch = false;

        $scope.pageChanged = function () {
            searchService.applyCurrentQueryToPages($scope.currentPage, $scope.pageSize);
        };

        $scope.$on('searchResultsChanged', function (event, queryResult) {
            $scope.employees = queryResult.employees;
            $scope.totalEmployees = queryResult.totalCount;
            $scope.isSearch = queryResult.isSearch;
            if (queryResult.isSearch) {
                $scope.searchQuery = queryResult.searchQuery;
            }
            $scope.currentPage = queryResult.page;
            $scope.pageSize = queryResult.pageSize;
            //maintain pagination state for future browsing or 'state' changes 
            //(note edge case - taking current value so it is possible these values could differ from the last search if currently in the middle of searching)
            $state.$current.data.pagination = {
                page: $scope.currentPage,
                pageSize: $scope.pageSize
            };
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

        if (!searchService.getLastQueryResult()) {
            //no queries have been run in the app yet (app load)
            searchService.query($scope.currentPage, $scope.pageSize);
        }
        else {
            //otherwise reload the last query
            if ($state.$current.data && $state.$current.data.pagination) {
                searchService.applyCurrentQueryToPages($state.$current.data.pagination.page, $state.$current.data.pagination.pageSize);
            }
        }
    }
})(angular.module('app'));
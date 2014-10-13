//This factory service (singleton in angular) is a state interface and container for search queries/results and broadcasting results

(function (app) {
    'use strict';

    app.factory('searchService', ['$rootScope', '$location', 'dataService', searchService]);

    function searchService($rootScope, $location, dataService) {
        var currentQuery = null;
        var lastQueryResult = null;

        var query = function (page, pageSize, location, searchQuery) {
            dataService.query(page, pageSize, location, searchQuery)
                .then(function (data) {
                    lastQueryResult = {
                        employees : data.result,
                        totalCount: data.totalCount
                    };
                    $location.path('');
                    $rootScope.$broadcast('searchResultsChanged', lastQueryResult);
                });

            currentQuery = {
                location: location,
                searchQuery: searchQuery
            };
        };

        var applyCurrentQueryToPages = function (page, pageSize) {
            if (currentQuery) {
                dataService.query(page, pageSize, currentQuery.location, currentQuery.searchQuery)
                    .then(function (data) {
                        lastQueryResult = {
                            employees: data.result,
                            totalCount: data.totalCount
                        };
                        $rootScope.$broadcast('searchResultsChanged', lastQueryResult);
                    });
            }
        };

        var editResultIfExists = function (employee) {
            if (results && results.length) {
                for (var i = 0; i < results.length; i++) {
                    if (results[i].employeeId === employee.employeeId) {
                        results[i] = employee;
                    }
                }
                $rootScope.$broadcast('searchResultsChanged', results);
            }
        };

        return {
            query: query,
            editResultIfExists: editResultIfExists,
            applyCurrentQueryToPages: applyCurrentQueryToPages
        };
    }
})(angular.module('app'));
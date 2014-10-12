//This factory service (singleton in angular) is a state container for search results and broadcasting changes to them (edits)

(function (app) {
    'use strict';

    app.factory('searchService', ['$rootScope', 'dataService', searchService]);

    function searchService($rootScope, dataService) {
        var currentQuery = null;
        //var currentQueryResults = null;
        var lastQueryResult = null;

        var query = function (page, pageSize, location, searchQuery) {
            //1st make sure there are no results in memory for it
            dataService.query(page, pageSize, location, searchQuery)
                .then(function (data) {
                    lastQueryResult = {
                        employees : data.result,
                        totalCount: data.totalCount
                    };
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

        //function updateCurrentQuery()

        return {
            query: query,
            editResultIfExists: editResultIfExists,
            applyCurrentQueryToPages: applyCurrentQueryToPages
        };
    }
})(angular.module('app'));
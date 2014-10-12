//This factory service (singleton in angular) is a state container for search results and broadcasting changes to them (edits)

(function (app) {
    'use strict';

    app.factory('searchService', ['$rootScope', 'dataService', searchService]);

    function searchService($rootScope, dataService) {
        var lastQueryResult = null;

        //var getQueryResult = function () {
        //    return queryResult;
        //};

        //var setQueryResult = function (result) {
        //    queryResult = result;
        //    $rootScope.$broadcast('searchResultsChanged', queryResult);
        //};

        var search = function (page, pageSize, location, searchQuery) {
            dataService.query(page, pageSize, location, searchQuery)
                .then(function (data) {
                    lastQueryResult = {
                        employees : data.result,
                        count: data.resultCount
                    };
                    $rootScope.$broadcast('searchResultsChanged', lastQueryResult);
                });
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
            //getQueryResult: getQueryResult,
            //setQueryResult: setQueryResult,
            search: search,
            lastQueryResult: lastQueryResult,
            editResultIfExists: editResultIfExists
        };
    }
})(angular.module('app'));
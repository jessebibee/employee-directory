//This factory service (singleton in angular) is a state container for search results and broadcasting changes to them (edits)

(function (app) {
    'use strict';

    app.factory('searchResultsService', ['$rootScope', searchResultsService]);

    function searchResultsService($rootScope) {
        var results = [];

        var getResults = function () {
            return results;
        };

        var setResults = function (employees) {
            results = employees;
            $rootScope.$broadcast('searchResultsChanged', employees);
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
            getResults: getResults,
            setResults: setResults,
            editResultIfExists: editResultIfExists
        };
    }
})(angular.module('app'));
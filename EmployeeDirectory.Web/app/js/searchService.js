//This factory service (singleton in angular) is a state interface and container for search queries/results and broadcasting results

(function (app) {
    'use strict';

    app.factory('searchService', ['$rootScope', '$location', 'dataService', searchService]);

    function searchService($rootScope, $location, dataService) {
        var currentQuery = {
            location: null,
            search: null
        };
        var lastQueryResult = null;

        var query = function (page, pageSize, location, searchQuery) {
            dataService.query(page, pageSize, location, searchQuery)
                .then(function (data) {
                    lastQueryResult = {
                        employees : data.result,
                        totalCount: data.totalCount,
                        isSearch: searchQuery && searchQuery.length > 0,
                        searchQuery: searchQuery,
                        page: page,
                        pageSize: pageSize
                    };
                    $location.path('');
                    $rootScope.$broadcast('searchResultsChanged', lastQueryResult);
                });
        };

        var applyCurrentQueryToPages = function (page, pageSize) {
            if (currentQuery) {
                dataService.query(page, pageSize, currentQuery.location, currentQuery.search)
                    .then(function (data) {
                        lastQueryResult = {
                            employees: data.result,
                            totalCount: data.totalCount,
                            isSearch: currentQuery.search && currentQuery.search.length > 0,
                            searchQuery: currentQuery.search,
                            page: page,
                            pageSize: pageSize
                        };
                        $rootScope.$broadcast('searchResultsChanged', lastQueryResult);
                    });
            }
        };

        var updateQueryLocation = function (location) {
            currentQuery.location = location;
        };

        var updateQuerySearch = function (search) {
            currentQuery.search = search;
        };

        var getLastQueryResult = function () {
            return lastQueryResult;
        };

        return {
            query: query,
            getLastQueryResult: getLastQueryResult,
            applyCurrentQueryToPages: applyCurrentQueryToPages,
            updateQueryLocation: updateQueryLocation,
            updateQuerySearch: updateQuerySearch
        };
    }
})(angular.module('app'));
(function (app) {
    'use strict';

    app.factory('dataService', ['$http', '$q', dataService]);

    function dataService($http, $q) {
        //will get enhanced with search criteria, for now get all
        var getAll = function () {
            var deferred = $q.defer();
            $http.get('/api/employees').
                success(function (data, status, headers, config) {
                    deferred.resolve(data);
                }).
                error(function (d) {
                    console.log('error', d);
                    deferred.reject(d);
                });
            return deferred.promise;
            //return $http.get('/api/employees');
        };
        
        var deleteEmployee = function (employeeId) {
            var deferred = $q.defer();
            $http.delete('/api/employees/' + employeeId).
                success(function (data, status, headers, config) {
                    deferred.resolve();
                }).
                error(function (data, status, headers, config) {
                    if (status === 404) {
                        //Going to consider 404 Not Found a success - the net result is the same
                        deferred.notify('Employee no longer exists'); //Note - create better API for this (some cases where consumer may be interested in this)
                        deferred.resolve();
                    }
                    deferred.reject();
                });
            return deferred.promise;
            //return $http.delete('/api/employees/' + employeeId);
        };

        return {
            searchEmployees: getAll,
            deleteEmployee: deleteEmployee
        };
    }
})(angular.module('app'));
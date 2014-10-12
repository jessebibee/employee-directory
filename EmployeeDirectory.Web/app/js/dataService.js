(function (app) {
    'use strict';

    app.factory('dataService', ['$http', '$q', dataService]);

    function dataService($http, $q) {
        var query = function (page, pageSize, location, searchQuery) {
            var query = '?page=' + page + '&pageSize=' + pageSize;
            if (location) {
                query = query + '&location=' + location;
            }
            if (searchQuery) {
                //decode and add to query
            }

            var deferred = $q.defer();
            $http.get('/api/employees' + query).
                success(function (data, status, headers, config) {
                    deferred.resolve(data);
                }).
                error(function (data, status, headers, config) {
                    deferred.reject(data);
                });
            return deferred.promise;
        };

        var getEmployee = function (employeeId) {
            var deferred = $q.defer();
            $http.get('/api/employees/' + employeeId).
                success(function (data, status, headers, config) {
                    deferred.resolve(data);
                }).
                error(function (data, status, headers, config) {
                    deferred.reject(data);
                });
            return deferred.promise;
        };

        var createEmployee = function (employee) {
            var deferred = $q.defer();
            $http.post('/api/employees', employee).
                success(function (data, status, headers, config) {
                    deferred.resolve(data);
                }).
                error(function (data, status, headers, config) {
                    deferred.reject(data);
                });
            return deferred.promise;
        };

        var editEmployee = function (employee) {
            var deferred = $q.defer();
            $http.put('/api/employees/' + employee.employeeId, employee).
                success(function (data, status, headers, config) {
                    deferred.resolve();
                }).
                error(function (data, status, headers, config) {
                    deferred.reject(data, status);
                });
            return deferred.promise;
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
        };

        return {
            query: query,
            getEmployee: getEmployee,
            createEmployee: createEmployee,
            editEmployee: editEmployee,
            deleteEmployee: deleteEmployee
        };
    }
})(angular.module('app'));
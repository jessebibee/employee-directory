(function () {
    'use strict';

    var app = angular.module('app', ['ui.router', 'ui.bootstrap']);

    app.config(['$stateProvider', '$locationProvider', configureRouting]);

    app.config(['$httpProvider', function($httpProvider) {
        $httpProvider.interceptors.push('authHttpResponseInterceptor');
    }]);

    function configureRouting($stateProvider, $locationProvider) {
        $locationProvider.html5Mode(true);

        $stateProvider.state('new-employee', {
            url: '/employees/new',
            templateUrl: '/app/templates/new-employee.html',
            controller: 'NewEmployeeController'
        });
        $stateProvider.state('employee-detail', {
            url: '/employees/:id',
            templateUrl: '/app/templates/employee-detail.html',
            controller: 'EmployeeDetailController',
            resolve: {
                employee: ['$stateParams', 'dataService', function ($stateParams, dataService) {
                    return dataService.getEmployee($stateParams.id); //re-load fresh copy 
                }]
            }
        });
        $stateProvider.state('directory', {
            url: '*path', //must be the last state declared - any after this will never get hit!!
            templateUrl: '/app/templates/directory.html',
            controller: 'DirectoryController',
            data: {
                searchResults: null
            }
        });
    }
})();
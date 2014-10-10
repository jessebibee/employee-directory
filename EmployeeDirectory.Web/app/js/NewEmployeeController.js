(function (app) {
    'use strict';

    app.controller('NewEmployeeController', ['$scope', 'dataService', NewEmployeeController]);

    function NewEmployeeController($scope, dataService) {
        console.log('hit new employee controller');
    }
})(angular.module('app'));
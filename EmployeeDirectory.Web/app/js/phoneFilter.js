(function (app) {
    'use strict';

    app.filter('phone', ['$filter', function ($filter) {
        return function (input) {
            if (input && input.length === 10) {
                var areaCode = input.substring(0, 3);
                var prefix = input.substring(3, 6);
                var number = input.substring(6, 10);
                return '(' + areaCode + ') ' + prefix + '-' + number;
            }
            return input;
        };
    }]);
})(angular.module('app'));
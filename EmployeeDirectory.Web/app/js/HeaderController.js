(function (app) {
    'use strict';

    app.controller('HeaderController', ['$scope', '$state', 'Identity', HeaderController]);

    function HeaderController($scope, $state, identity) {
        $scope.user = function () {
            return identity.user;
        };

        $scope.loggedIn = function () {
            return identity.loggedIn;
        };

        $scope.user = function () {
            return identity.user;
        };

        $scope.isEmployee = function () {
            return identity.hasRole('Employee');
        };

        $scope.loadUserDetail = function () {
            $state.go('employee-detail', { id: identity.user.employeeId });
        };
    }
})(angular.module('app'));
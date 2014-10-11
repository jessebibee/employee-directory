(function (app) {
    'use strict';

    app.factory('Identity', Identity);

    function Identity() {
        return {
            loggedIn: false,
            user: null,
            hasRole: function (role) {
                if (this.user && angular.isArray(this.user.roles)) {
                    if (this.user.roles.indexOf(role) > -1) {
                        return true;
                    }
                }
                return false;
            },
            logIn: function (user) {
                this.loggedIn = true;
                this.user = user;
            },
            logOut: function () {
                this.loggedIn = false;
                this.user = null;
            }
        };
    }
})(angular.module('app'));
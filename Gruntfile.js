module.exports = function (grunt) {
    'use strict';

    grunt.initConfig({
        karma: {
            unit: {
                configFile: 'EmployeeDirectory.Web.Test.Unit/app/karma.conf.js',
                autoWatch: false,
                singleRun: true
            }
        },
        jshint: {
            files: ['Gruntfile.js', 'EmployeeDirectory.Web/app/js/**/*.js', 'EmployeeDirectory.Web.Test.Unit/app/specs/**/*.js'],
            options: {
                globals: {
                    jQuery: true,
                    angular: true
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-karma');
    grunt.loadNpmTasks('grunt-contrib-jshint');

    grunt.registerTask('test:unit', ['karma:unit']);
    grunt.registerTask('lint', ['jshint']);
    grunt.registerTask('default', ['jshint', 'test:unit']);
};

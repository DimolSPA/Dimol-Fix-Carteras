//Gulp configuration file
var gulp = require('gulp'),
    concat = require('gulp-concat'),
    rename = require('gulp-rename'),
    //clean = require('gulp-clean');
    sourcemaps = require('gulp-sourcemaps'),
    uglify = require('gulp-uglify');

//Config object
var config = {
    src: ['modules/*.js'],
    dest: '../',
    outputName: 'site'
};

//Concat and uglify all functional files into site.js and site.min.js files
gulp.task('dimol-js', function () {
    return gulp.src(config.src)
        .pipe(sourcemaps.init())
        .pipe(concat(config.outputName + '.js'))
        .pipe(gulp.dest(config.dest))
        .pipe(rename(config.outputName + '.min.js'))
        .pipe(uglify())
        .pipe(sourcemaps.write())
        .pipe(gulp.dest(config.dest))
});

//Clear dest folder before run dimol-js task
//gulp.task('clear-dest', function () {
//    return gulp.src(config.dest, {read: false})
//        .pipe(clean());
//});

////Move result to /Scripts
//gulp.task('move-result', function () {
//    return gulp.src(config.dest + "**.*")
//        .pipe(gulp.dest('../'));
//});

//Watch changes and re-run principal tasks
gulp.task('watch', function () {
    return gulp.watch(config.src, ['default']);
});


//Default task
gulp.task('default', ['dimol-js'], function () { });
var gulp = require('gulp'),
    cleanCss = require('gulp-clean-css'),
    scss = require('gulp-sass');

scss.compiler = require('node-sass');

var browserify = require('browserify');
var source = require('vinyl-source-stream');
var tsify = require('tsify');
var uglify = require('gulp-uglify');
var sourcemaps = require('gulp-sourcemaps');
var buffer = require('vinyl-buffer');


var out_js = "./";
var out_css = "./";

gulp.task('scss', function () {
    return gulp
        .src('src/scss/index.scss')
        .pipe(scss().on('error', scss.logError))
        .pipe(sourcemaps.init({ loadMaps: true }))
        .pipe(sourcemaps.write('./'))
        .pipe(cleanCss({ compatibility: 'ie8' }))
        .pipe(gulp.dest(out_css));
});

gulp.task('ts', function () {
    return browserify({
        basedir: '.',
        debug: true,
        entries: ['./src/ts/index.ts'],
        cache: {},
        packageCache: {}
    })
        .plugin(tsify)
        .transform('babelify', {
            presets: ['es2015'],
            extensions: ['.ts']
        })
        .bundle()
        .pipe(source('index.js'))
        .pipe(buffer())
        .pipe(sourcemaps.init({ loadMaps: true }))
        .pipe(uglify())
        .pipe(sourcemaps.write('./'))
        .pipe(gulp.dest(out_js));
});

gulp.task('default', gulp.parallel('scss', 'ts'), function () { });
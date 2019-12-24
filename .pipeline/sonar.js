'use strict';
const task = require('./lib/sonar.js')
const settings = require('./lib/config.js')

task(Object.assign(settings, { phase: 'sonar'}))

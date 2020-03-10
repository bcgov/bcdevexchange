'use strict';
const task = require('./lib/matomo.js')
const settings = require('./lib/config.js')

task(Object.assign(settings, { phase: 'matomo'}))

'use strict';
const {OpenShiftClientX} = require('pipeline-cli')
const path = require('path');

module.exports = (settings)=>{
  const phases = settings.phases
  const options = settings.options
  const oc=new OpenShiftClientX(Object.assign({'namespace':phases.build.namespace}, options));
  const phase='matomo'
  let objects = []
  const templatesLocalBaseUrl =oc.toFileUrl(path.resolve(__dirname, '../../openshift/matomo'))

  objects.push(...oc.processDeploymentTemplate(`${templatesLocalBaseUrl}/mariadb-build.yaml`, {
    'param':{}
}))

  objects.push(...oc.processDeploymentTemplate(`${templatesLocalBaseUrl}/matomo-build.yaml`, {
    'param':{}
}))

  objects.push(...oc.processDeploymentTemplate(`${templatesLocalBaseUrl}/matomo-db-deploy.yaml`, {
    'param':{
      'TAG_NAME' : 'latest'
    }
}))

objects.push(...oc.processDeploymentTemplate(`${templatesLocalBaseUrl}/matomo-proxy-build.yaml`, {
  'param':{
    'GIT_REF': oc.git.ref
  }
}))

  objects.push(...oc.processDeploymentTemplate(`${templatesLocalBaseUrl}/matomo-deploy.yaml`, {
    'param':{
      'TAG_NAME' : '3.11.0-fpm',
      'PROXY_TAG_NAME' : 'latest',
      'MATOMO_URL' : 'matomo.pathfinder.gov.bc.ca'
    }
}))

  oc.applyRecommendedLabels(objects, phases[phase].name, phase, phases[phase].changeId, phases[phase].instance)
  oc.applyAndBuild(objects)
}
'use strict';
const {OpenShiftClientX} = require('pipeline-cli')
const path = require('path');

module.exports = (settings)=>{
  const phases = settings.phases
  const options = settings.options
  const oc=new OpenShiftClientX(Object.assign({'namespace':phases.build.namespace}, options));
  const phase='sonar'
  const sonarPwd=options.sonarPwd
  const sonarUrl=options.sonarUrl
  const projectName=options.project
  const changeId=options.pr
  let objects = []
  const templatesLocalBaseUrl =oc.toFileUrl(path.resolve(__dirname, '../../openshift'))

  objects.push(...oc.processDeploymentTemplate(`${templatesLocalBaseUrl}/bcdevexchange-sonar-build.yaml`, {
    'param':{
      'NAME': phases[phase].name,
      'SOURCE_REPOSITORY_URL': oc.git.http_url,
      'SOURCE_REPOSITORY_REF': oc.git.ref,
      'SOURCE_CONTEXT_DIR': 'dotnet-sonar'
    }
}))

  objects.push(...oc.processDeploymentTemplate(`${templatesLocalBaseUrl}/bcdevexchange-sonar.yaml`, {
    'param':{
      'NAME': 'bcdevexchange-sonar',
      'MEMORY_REQUEST':phases[phase].memoryrequest,
      'MEMORY_LIMIT':phases[phase].memorylimit,
      'CPU_REQUEST':phases[phase].cpurequest,
      'CPU_LIMIT':phases[phase].cpulimit,
      'SOURCE_REPOSITORY_URL': oc.git.http_url,
      'SOURCE_REPOSITORY_REF': oc.git.ref,
      'SOURCE_CONTEXT_DIR': '',
      'SONAR_URL': sonarUrl,
      'SONAR_PWD': sonarPwd,
      'SONAR_PROJECT': projectName,
      'CHANGE_ID': changeId
    }
}))

  oc.applyRecommendedLabels(objects, phases[phase].name, phase, phases[phase].changeId, phases[phase].instance)
  oc.applyAndBuild(objects)
}
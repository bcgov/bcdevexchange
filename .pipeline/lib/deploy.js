'use strict';
const {OpenShiftClientX} = require('pipeline-cli')
const path = require('path');

module.exports = (settings)=>{
  const phases = settings.phases
  const options= settings.options
  const phase=options.env
  const bearerToken = options.bt;
  const changeId = phases[phase].changeId
  const oc=new OpenShiftClientX(Object.assign({'namespace':phases[phase].namespace}, options));
  const templatesLocalBaseUrl =oc.toFileUrl(path.resolve(__dirname, '../../openshift'))
  var objects = []

  objects.push(...oc.processDeploymentTemplate(`${templatesLocalBaseUrl}/bcdevexchange-deploy.yaml`, {
    'param':{
      'NAME': phases[phase].name,
      'SUFFIX': phases[phase].suffix,
      'VERSION': phases[phase].version,
      'HOST': `${phases[phase].name}${phases[phase].suffix}-${phases[phase].namespace}.pathfinder.gov.bc.ca`,
      'ASPNETCORE_ENVIRONMENT': phases[phase].aspdotnetenvironment,
      'BEARER_TOKEN': bearerToken,
      'MIN_REPLICAS': phases[phase].minreplicas,
      'MAX_REPLICAS': phases[phase].maxreplicas,
      'MEMORY_REQUEST':phases[phase].memoryrequest,
      'MEMORY_LIMIT':phases[phase].memorylimit,
      'CPU_REQUEST':phases[phase].cpurequest,
      'CPU_LIMIT':phases[phase].cpulimit
    }
}))

  oc.applyRecommendedLabels(objects, phases[phase].name, phase, `${changeId}`, phases[phase].instance)
  oc.importImageStreams(objects, phases[phase].tag, phases.build.namespace, phases.build.tag)
  oc.applyAndDeploy(objects, phases[phase].instance)
}

"use strict";
const options = require("pipeline-cli").Util.parseArguments();
const changeId = options.pr; //aka pull-request
const version = "1.0.0";
const name = "bcdevexchange";

const phases = {
  build: {
    namespace: "ifttgq-tools",
    name: `${name}`,
    phase: "build",
    changeId: changeId,
    suffix: `-build-${changeId}`,
    instance: `${name}-build-${changeId}`,
    version: `${version}-${changeId}`,
    tag: `build-${version}-${changeId}`,
    aspdotnetenvironment: "Development",
    minreplicas: "1",
    maxreplicas: "1",
    memoryrequest: "256Mi",
    memorylimit: "1Gi",
    cpurequest: "100m",
    cpulimit: "500m"
  },
  dev: {
    namespace: "ifttgq-dev",
    name: `${name}`,
    phase: "dev",
    changeId: changeId,
    suffix: `-dev-${changeId}`,
    instance: `${name}-dev-${changeId}`,
    version: `${version}-${changeId}`,
    tag: `dev-${version}-${changeId}`,
    aspdotnetenvironment: "Development",
    minreplicas: "1",
    maxreplicas: "2",
    memoryrequest: "256Mi",
    memorylimit: "1Gi",
    cpurequest: "100m",
    cpulimit: "500m"
  },
  test: {
    namespace: "ifttgq-test",
    name: `${name}`,
    phase: "test",
    changeId: changeId,
    suffix: `-test`,
    instance: `${name}-test`,
    version: `${version}-${changeId}`,
    tag: `test-${version}`,
    aspdotnetenvironment: "Staging",
    minreplicas: "2",
    maxreplicas: "6",
    memoryrequest: "1Gi",
    memorylimit: "2Gi",
    cpurequest: "100m",
    cpulimit: "1"
  },
  prod: {
    namespace: "ifttgq-prod",
    name: `${name}`,
    phase: "prod",
    changeId: changeId,
    suffix: `-prod`,
    instance: `${name}-prod`,
    version: `${version}-${changeId}`,
    tag: `prod-${version}`,
    aspdotnetenvironment: "Production",
    minreplicas: "2",
    maxreplicas: "6",
    memoryrequest: "2Gi",
    memorylimit: "4Gi",
    cpurequest: "200m",
    cpulimit: "1"
  }
};

// This callback forces the node process to exit as failure.
process.on("unhandledRejection", reason => {
  console.log(reason);
  process.exit(1);
});

module.exports = exports = { phases, options };

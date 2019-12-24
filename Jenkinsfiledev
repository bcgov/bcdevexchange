import bcgov.GitHubHelper

def SONAR_ROUTE_NAME = 'sonarqube'

def SONAR_ROUTE_NAMESPACE = 'ifttgq-tools'

def SONAR_PROJECT_NAME = 'BC Dev Exchange'

def SONAR_PROJECT_KEY = 'bcdevexchange'

def SONAR_PROJECT_BASE_DIR = '../'

def SONAR_SOURCES = './bcdevexchange'

def SOURCE_REPO_REF = "pull/${CHANGE_ID}/head"

def APP_SITE = "https://bcdevexchange-dev-" 
def DEV_URL = "-ifttgq-dev.pathfinder.gov.bc.ca/"

// Gets the URL associated to a named route.
@NonCPS
String getUrlForRoute(String routeName, String projectNameSpace = '') {

  def nameSpaceFlag = ''
  if(projectNameSpace?.trim()) {
    nameSpaceFlag = "-n ${projectNameSpace}"
  }
  
  def url = sh (
    script: "oc get routes ${nameSpaceFlag} -o wide --no-headers | awk \'/${routeName}/{ print match(\$0,/edge/) ?  \"https://\"\$2 : \"http://\"\$2 }\'",
    returnStdout: true
  ).trim()

  return url
}

@NonCPS
String getSonarQubePwd() {

  sonarQubePwd = sh (
    script: 'oc get secret sonarqube-admin-password -o go-template --template="{{.data.password|base64decode}}"',
    returnStdout: true
  ).trim()

  return sonarQubePwd
}

// Notify stage status and pass to Jenkins-GitHub library
def notifyStageStatus(String name, String status) {
  def sha1 = GIT_COMMIT
  if(env.CHANGE_TARGET != 'master') {
    sha1 = GitHubHelper.getPullRequestLastCommitId(this)
  }

  GitHubHelper.createCommitStatus(
    this, sha1, status, BUILD_URL, '', "Stage: ${name}"
  )
}

// Create deployment status and pass to Jenkins-GitHub library
def createDeploymentStatus(String environment, String status) {
  def ghDeploymentId = new GitHubHelper().createDeployment(
    this,
    SOURCE_REPO_REF,
    [
      'environment': environment,
      'task': "deploy:pull:${CHANGE_ID}"
    ]
  )

  new GitHubHelper().createDeploymentStatus(
    this,
    ghDeploymentId,
    status,
    ['targetUrl': "https://${APP_SITE}${CHANGE_ID}${DEV_URL}"]
  )

  if (status.equalsIgnoreCase('SUCCESS')) {
    echo "${environment} deployment successful at https://${APP_SITE}${CHANGE_ID}${DEV_URL}"
  } else if (status.equalsIgnoreCase('PENDING')) {
    echo "${environment} deployment pending..."
  } else if (status.equalsIgnoreCase('FAILURE')) {
    echo "${environment} deployment failed"
  }
}

// Creates a comment and pass to Jenkins-GitHub library
def commentOnPR(String comment) {
  if(env.CHANGE_TARGET != 'master') {
    GitHubHelper.commentOnPullRequest(this, comment)
  }
}


pipeline {
    agent none
    options {
        disableResume()
    }
    stages {
        stage('SonarScan') {
            agent { label 'build' }
            steps {
                notifyStageStatus('SonarScan', 'PENDING')
                script{
                    echo "Performing static SonarQube code analysis ..."
                    SONARQUBE_URL = getUrlForRoute(SONAR_ROUTE_NAME, SONAR_ROUTE_NAMESPACE).trim()
                    SONARQUBE_PROJECT = "BCDevExchange"
                    if (env.CHANGE_TARGET != "master")
                    {
                        SONARQUBE_PROJECT = SONARQUBE_PROJECT + "=PR-${CHANGE_ID}"
                    }

                    SONARQUBE_PWD = getSonarQubePwd().trim()
                    echo "URL: ${SONARQUBE_URL}"
                    echo "PWD: ${SONARQUBE_PWD}"
                    sh "cd .pipeline && chmod +777 npmw && ./npmw ci && ./npmw run sonar -- --pr=${CHANGE_ID} --sonarUrl=${SONARQUBE_URL} --sonarPwd=${SONARQUBE_PWD} --project=${SONARQUBE_PROJECT}"
                }
            }  
            post {
                sucess{
                    notifyStageStatus('SonarScan', 'SUCCESS')
                }
                failure{
                    notifyStageStatus('SonarScan', 'FAILURE')
                }
                changed {
                    script{
                        // Comment on Pull Request if build changes to successful
                        if(currentBuild.currentResult.equalsIgnoreCase('SUCCESS')) {
                            echo "Posting SonarQube Analysis: ${SONARQUBE_URL}/dashboard?id=${SONARQUBE_PROJECT}"
                            commentOnPR("SonarQube Analysis: ${SONARQUBE_URL}/dashboard?id=${SONARQUBE_PROJECT}")
                        }
                    }
                }
            } 
        }
        stage('Build') {
            agent { label 'build' }
            steps {
                notifyStageStatus('Build', 'PENDING')
                script {
                    def filesInThisCommitAsString = sh(script:"git diff --name-only HEAD~1..HEAD", returnStatus: false, returnStdout: true).trim()
                    def hasChangesInPath = (filesInThisCommitAsString.length() > 0)
                    echo "${filesInThisCommitAsString}"
                    if (!currentBuild.rawBuild.getCauses()[0].toString().contains('UserIdCause') && !hasChangesInPath){
                        currentBuild.rawBuild.delete()
                        error("No changes detected in the path")
                    }
                }
                echo "Aborting all running jobs ..."
                script {
                    abortAllPreviousBuildInProgress(currentBuild)
                }
                echo "Building ..."
                sh "cd .pipeline && chmod +777 npmw && ./npmw ci && ./npmw run build -- --pr=${CHANGE_ID}"
            }
            post {
                sucess{
                    notifyStageStatus('Build', 'SUCCESS')
                }
                failure{
                    notifyStageStatus('Build', 'FAILURE')
                }
            }
        }
        stage('Deploy (DEV)') {
            agent { label 'deploy' }
            steps {
                notifyStageStatus('Deploy(Dev)', 'PENDING')
                echo "Deploying ..."
                sh "cd .pipeline && chmod +777 npmw && ./npmw ci && ./npmw run deploy -- --pr=${CHANGE_ID} --env=dev"
            }
            post {
                sucess{
                    createDeploymentStatus("Dev", 'SUCCESS')
                    notifyStageStatus('Deploy(Dev)', 'SUCCESS')
                }
                failure{
                    createDeploymentStatus("Dev", 'FAILURE')
                    notifyStageStatus('Deploy(Dev)', 'FAILURE')
                }
            }
        }
        stage('Deploy (TEST)') {
            agent { label 'deploy' }
            when {
                expression { return env.CHANGE_TARGET == 'master';}
                beforeInput true
            }
            input {
                message "Should we continue with deployment to TEST?"
                ok "Yes!"
            }
            steps {
                echo "Deploying ..."
                sh "cd .pipeline && chmod +777 npmw && ./npmw ci && ./npmw run deploy -- --pr=${CHANGE_ID} --env=test"
            }
        }
        stage('Deploy (PROD)') {
            agent { label 'deploy' }
            when {
                expression { return env.CHANGE_TARGET == 'master';}
                beforeInput true
            }
            input {
                message "Should we continue with deployment to PROD?"
                ok "Yes!"
            }
            steps {
                echo "Deploying ..."
                sh "cd .pipeline && chmod +777 npmw && ./npmw ci && ./npmw run deploy -- --pr=${CHANGE_ID} --env=prod"
            }
        }
    }
}
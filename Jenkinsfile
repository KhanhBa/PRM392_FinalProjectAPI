pipeline {
    agent any

    stages {
      
        stage('Packaging') {
            steps {
                sh 'docker build --pull --rm -f Dockerfile -t api_prm:latest .'
            }
        }

        stage('Push to DockerHub') {
            steps {
                withDockerRegistry(credentialsId: 'dockerhub', url: 'https://index.docker.io/v1/') {
                    sh 'docker tag api_prm:latest tuanhuu3264/api_prm:latest'
                    sh 'docker push tuanhuu3264/api_prm:latest'
                }
            }
        }

        stage('Deploy FE to DEV') {
            steps {
                echo 'Deploying and cleaning'
                echo "${env.DB_USER}"
                sh 'if [ $(docker ps -q -f name=api_prm) ]; then docker container stop api_prm; fi'
                sh 'echo y | docker system prune'
                sh 'docker container run -d --name api_prm -p 7666:8080 -p 7667:8081 tuanhuu3264/api_prm'
            }
        }
    }

    post {
        always {
            cleanWs()
        }
    }
}

pipeline {
    agent any

    environment {
        DOTNET = "C:\\Program Files\\dotnet\\dotnet.exe"
        PUBLISH_DIR = "D:\\Programming\\Practice\\Publish\\OrderService" 
        IIS_SITE = "OrderService"
        IIS_PATH = "D:\\AppPublish\\OrderService"
    }

    stages {

       /*  stage('Checkout') {
            steps {
                git branch: 'main',
                    url: 'https://github.com/<badshabati10>/OrderService.git'
            }
        } */

        stage('Restore') {
            steps {
                bat "\"%DOTNET%\" restore"
            }
        }

        stage('Build') {
            steps {
                bat "\"%DOTNET%\" build -c Release --no-restore"
            }
        }

        stage('Publish') {
            steps {
                bat "\"%DOTNET%\" publish -c Release -o %PUBLISH_DIR% --no-build"
            }
        }

        stage('Deploy to IIS') {
            steps {
                echo "Stopping IIS site"
                bat "powershell -Command \"Stop-WebSite -Name '%IIS_SITE%'\""

                echo "Copying files"
                bat "xcopy %PUBLISH_DIR%\\* %IIS_PATH%\\ /E /Y /I"

                echo "Starting IIS site"
                bat "powershell -Command \"Start-WebSite -Name '%IIS_SITE%'\""
            }
        }

        stage('Health Check') {
            steps {
                bat 'powershell -Command "try { $r = Invoke-WebRequest %HEALTH_URL% -UseBasicParsing -TimeoutSec 10; if ($r.StatusCode -ne 200) { exit 1 } } catch { exit 1 }"'
            }
        }
    }

    post {
        success {
            echo "✅ Deployment successful"
        }
        failure {
            echo "❌ Deployment failed"
        }
    }
}

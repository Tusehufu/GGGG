<template>
    <div class="col-md-12">
        <div class="card card-container">
            <Form @submit="handleLogin" :validation-schema="schema">
                <div class="form-group">
                    <label for="username">Användarnamn</label>
                    <Field name="username" v-model="userCredentials.username" type="text" class="form-control" />
                    <ErrorMessage name="username" class="error-feedback" />
                </div>
                <div class="form-group">
                    <label for="password">Lösenord</label>
                    <Field name="password" v-model="userCredentials.password" type="password" class="form-control" />
                    <ErrorMessage name="password" class="error-feedback" />
                </div>

                <div class="form-group">
                    <button class="btn btn-primary btn-block" :disabled="loading">
                        <span v-show="loading" class="spinner-border spinner-border-sm"></span>
                        <span>Logga in</span>
                    </button>
                </div>

                <div class="form-group">
                    <div v-if="errorMessage" class="alert alert-danger" role="alert">
                        {{ errorMessage }}
                    </div>
                </div>
            </Form>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent, ref } from 'vue';
    import axios from 'axios';
    import { Form, Field, ErrorMessage } from 'vee-validate';
    import * as yup from 'yup';

    export default defineComponent({
        name: 'Login',
        components: {
            Form,
            Field,
            ErrorMessage,
        },
        setup() {
            const userCredentials = ref({
                username: '',
                password: '',
            });

            const loading = ref(false);
            const errorMessage = ref('');

            const schema = yup.object().shape({
                username: yup.string().required('Användarnamn krävs'),
                password: yup.string().required('Lösenord krävs'),
            });

            const handleLogin = async () => {
                try {
                    loading.value = true;
                    errorMessage.value = '';

                    // Logga användaruppgifterna som skickas till API:et
                    console.log('Användaruppgifter som skickas till API:', userCredentials.value);

                    // Gör API-anropet
                    const response = await axios.post('https://localhost:7056/api/auth/login', userCredentials.value);

                    // Hantera svar från servern
                    if (response.status === 200) {
                        console.log('Inloggning lyckades:', response.data);

                        // Återställa formuläret och omdirigera användaren
                        userCredentials.value = {
                            username: '',
                            password: '',
                        };
                    } else {
                        // Om svaret inte är 200, kasta ett fel
                        throw new Error('Inloggning misslyckades');
                    }
                } catch (error) {
                    console.error('Fel vid inloggning:', error);
                    errorMessage.value = 'Fel vid inloggning. Kontrollera dina inloggningsuppgifter och försök igen.';
                } finally {
                    loading.value = false;
                }
            };

            return {
                userCredentials,
                loading,
                errorMessage,
                schema,
                handleLogin,
            };
        },
    });
</script>

<style scoped>
    .error-feedback {
        color: red;
    }
</style>

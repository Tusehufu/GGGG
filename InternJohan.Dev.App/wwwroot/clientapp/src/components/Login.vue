﻿<template>
    <div class="col-md-12">
        <div class="card card-container">
            <!-- Visa inloggningsformulär om användaren inte är inloggad -->
            <Form v-if="!isLoggedIn" @submit="handleLogin" :validation-schema="schema">
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

            <!-- Visa inloggad text och knappar om användaren är inloggad -->
            <div v-else>
                <p>Du är inloggad</p>
                <button @click="handleLogout" class="btn btn-danger">Logga ut</button>
                <!-- Visa bekräftelsemeddelande om användaren har loggats ut -->
                <p v-if="loggedOut" class="text-success">Du har loggats ut</p>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent, ref, computed } from 'vue';
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
            const loggedOut = ref(false); // Ref för att hålla koll på om användaren har loggats ut

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

                    // Gör API-anropet för att logga in
                    const loginResponse = await axios.post('https://localhost:7056/api/auth/login', userCredentials.value);

                    // Hantera svar från servern
                    if (loginResponse.status === 200) {
                        console.log('Inloggning lyckades:', loginResponse.data);

                        // Spara JWT-token i localStorage
                        const token = loginResponse.data.token;
                        localStorage.setItem('jwtToken', token);

                        // Hämta användar-ID baserat på användarnamnet
                        const username = userCredentials.value.username;
                        const userIdResponse = await axios.get(`https://localhost:7056/api/users/id?username=${username}`);

                        if (userIdResponse.status === 200) {
                            const userId = userIdResponse.data;
                            console.log('Användar-ID hämtat:', userId);
                            localStorage.setItem('userId', userId);
                        } else {
                            throw new Error('Kunde inte hämta användar-ID.');
                        }

                        window.location.reload();
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

            const handleLogout = () => {
                // Ta bort JWT-token och användar-ID från localStorage för att logga ut användaren
                localStorage.removeItem('jwtToken');
                localStorage.removeItem('userId');
                // Sätt loggedOut till true för att visa bekräftelsemeddelandet
                loggedOut.value = true;
                // Återställ användaruppgifterna
                userCredentials.value = {
                    username: '',
                    password: '',
                };
                window.location.reload();
            };

            // Kolla om användaren är inloggad baserat på userId i local storage
            const isLoggedIn = computed(() => !!localStorage.getItem('userId'));

            return {
                userCredentials,
                loading,
                errorMessage,
                schema,
                handleLogin,
                handleLogout,
                isLoggedIn,
                loggedOut,
            };
        },
    });
</script>

<style scoped>
    .error-feedback {
        color: red;
    }
</style>

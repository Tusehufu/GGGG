<template>
    <div class="modal show" style="display: block;" v-if="isVisible">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Redigera evenemang</h5>
                    <button type="button" class="btn-close" @click="closeModal"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="saveEvent">
                        <div class="mb-3">
                            <label for="sport" class="form-label">Sport</label>
                            <input type="text" id="sport" v-model="editEvent.sport" class="form-control" required />
                        </div>
                        <div class="mb-3">
                            <label for="neededParticipants" class="form-label">Antal deltagare som behövs</label>
                            <input type="number" id="neededParticipants" v-model="editEvent.neededParticipants" class="form-control" required />
                        </div>
                        <div class="mb-3">
                            <label for="participants" class="form-label">Antal deltagare just nu</label>
                            <input type="number" id="participants" v-model="editEvent.participants" class="form-control" />
                        </div>
                        <div class="mb-3">
                            <label for="dateTime" class="form-label">Datum och tid</label>
                            <input type="datetime-local" id="dateTime" v-model="editEvent.dateTime" class="form-control" required />
                        </div>
                        <div class="mb-3">
                            <label for="location" class="form-label">Plats</label>
                            <input type="text" id="location" v-model="editEvent.location" class="form-control" required />
                        </div>
                        <button type="submit" class="btn btn-primary">Spara</button>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';

export default defineComponent({
    name: 'EditEventModal',
    props: {
        isVisible: {
            type: Boolean,
            required: true,
        },
        editEvent: {
            type: Object as PropType<any>,
            required: true,
        },
    },
    emits: ['close', 'save'],
    setup(props, { emit }) {
        // Funktion för att spara ändringarna
        const saveEvent = () => {
            emit('save');
        };

        // Funktion för att stänga modalen
        const closeModal = () => {
            emit('close');
        };

        return {
            saveEvent,
            closeModal,
        };
    },
});
</script>

<style>
    /* Lägg till din CSS här om du vill */
</style>

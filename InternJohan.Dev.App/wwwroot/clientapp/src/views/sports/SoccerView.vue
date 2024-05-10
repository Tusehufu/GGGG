<template>
    <div class="booking">
        <h1 class="text-center">Välkommen till alla idrottsevenemang</h1>
        <h2 class="text-center">Här kan du se alla idrottsevenemang och gå med i idrottsevenemanget</h2>
        <div class="container">
            <div class="row">
                <div v-for="sportevent in sportevents" :key="sportevent.id" class="col">
                    <div class="card mt-3" style="width: 18rem;">
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item">Sport: {{ sportevent.sport }}</li>
                            <li class="list-group-item">Antal deltagare: {{ sportevent.participants }}</li>
                            <li class="list-group-item">Antal deltagare som behövs: {{ sportevent.neededParticipants }}</li>
                            <li class="list-group-item">Tid: {{ sportevent.dateTime }}</li>
                            <li class="list-group-item">Plats: {{ sportevent.location }}</li>
                            <li class="list-group-item d-none">ID: {{ sportevent.id }}</li>
                        </ul>
                        <div class="card-footer">
                            <button class="btn btn-success me-1">Gå med</button>
                            <button class="btn btn-danger me-1" @click="openConfirmDeleteModal(sportevent.id)">Radera</button>
                            <button class="btn btn-primary" @click="openEditModal(sportevent)">Redigera</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <EditEventModal :isVisible="isModalVisible"
                    :editEvent="editEvent"
                    @close="closeModal"
                    @eventUpdated="handleEventUpdated" />
    <ConfirmDeleteModal :isVisible="isConfirmDeleteVisible"
                        :eventId="eventIdToDelete"
                        @confirm="deleteSportEvent"
                        @cancel="closeConfirmDeleteModal" />
</template>

<script lang="ts">
    import { defineComponent, ref, onMounted } from 'vue';
    import axios from 'axios';
    import EditEventModal from '../../components/EditEventModal.vue';
    import ConfirmDeleteModal from '../../components/ConfirmDeleteModal.vue';

    export default defineComponent({
        name: 'SoccerView',
        components: {
            EditEventModal,
            ConfirmDeleteModal,
        },
        setup() {
            const sportevents = ref([]);
            const isModalVisible = ref(false);
            const editEvent = ref<any>({});
            const isConfirmDeleteVisible = ref(false);
            const eventIdToDelete = ref<number | null>(null);

            const getSportEvents = async () => {
                try {
                    const response = await axios.get('https://localhost:7056/api/SportEvent');
                    sportevents.value = response.data;
                } catch (error) {
                    console.error('Fel vid hämtning av sportevenemang:', error);
                }
            };

            const openEditModal = (sportevent: any) => {
                editEvent.value = sportevent;
                isModalVisible.value = true;
            };

            const closeModal = () => {
                isModalVisible.value = false;
            };

            const handleEventUpdated = () => {
                getSportEvents();
                isModalVisible.value = false;
            };

            const openConfirmDeleteModal = (eventId: number) => {
                eventIdToDelete.value = eventId;
                isConfirmDeleteVisible.value = true;
            };

            const closeConfirmDeleteModal = () => {
                isConfirmDeleteVisible.value = false;
            };

            const deleteSportEvent = async (eventId: number) => {
                try {
                    await axios.delete(`https://localhost:7056/api/SportEvent/${eventId}`);
                    await getSportEvents();
                } catch (error) {
                    console.error('Fel vid borttagning av sportevenemang:', error);
                }
                closeConfirmDeleteModal();
            };

            onMounted(() => {
                getSportEvents();
            });

            return {
                sportevents,
                isModalVisible,
                editEvent,
                isConfirmDeleteVisible,
                eventIdToDelete,
                openEditModal,
                closeModal,
                handleEventUpdated,
                openConfirmDeleteModal,
                closeConfirmDeleteModal,
                deleteSportEvent,
            };
        },
    });
</script>
